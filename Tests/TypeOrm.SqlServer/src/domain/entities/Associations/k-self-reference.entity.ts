import { K_SelfReference } from './k-self-reference.entity';
import { Entity, PrimaryGeneratedColumn, Column, ManyToOne } from 'typeorm';

@Entity('k_self_reference')
export class K_SelfReference {

  @PrimaryGeneratedColumn('uuid')
  id?: string;
  @Column()
  selfRefAttr: string;

  @ManyToOne(() => K_SelfReference, { cascade: ['insert', 'update'] })
  k_SelfReference?: K_SelfReference;

  @Column({ nullable: true })
  createdBy?: string;

  @Column({ nullable: true })
  createdDate?: Date;

  @Column({ nullable: true })
  lastModifiedBy?: string;

  @Column({ nullable: true })
  lastModifiedDate?: Date;
}