import { G_MultipleDependent } from './g-multiple-dependent.entity';
import { Entity, PrimaryGeneratedColumn, Column, OneToMany } from 'typeorm';

@Entity('g_required_composite_nav')
export class G_RequiredCompositeNav {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  reqCompNavAttr: string;

  @OneToMany(() => G_MultipleDependent, (g_MultipleDependents) => g_MultipleDependents.g_RequiredCompositeNav, { cascade: true, eager: true })
  g_MultipleDependents: G_MultipleDependent[];

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}