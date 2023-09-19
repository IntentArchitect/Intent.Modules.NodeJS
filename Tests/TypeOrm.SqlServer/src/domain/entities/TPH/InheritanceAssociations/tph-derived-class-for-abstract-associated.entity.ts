import { TphDerivedClassForAbstract } from './tph-derived-class-for-abstract.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('tph_derived_class_for_abstract_associated')
export class TphDerivedClassForAbstractAssociated {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  associatedField: string;

  @Column()
  derivedClassForAbstractId: string;

  @ManyToOne(() => TphDerivedClassForAbstract, { cascade: ['insert', 'update'], nullable: false })
  derivedClassForAbstract: TphDerivedClassForAbstract;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}